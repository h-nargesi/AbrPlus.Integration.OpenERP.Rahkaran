using AbrPlus.Integration.OpenERP.Hosting.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeptaKit.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public abstract class BaseRahkaranRepository<TEntity, TKey>(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseOpenErpApiRepository<RahkaranDbContext, TEntity, TKey>(dbContext as RahkaranDbContext, loggerFactory)
    where TEntity : BaseEntity<TKey>
{
}

public abstract class BaseRahkaranRepository<TEntity>(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseOpenErpApiRepository<RahkaranDbContext, TEntity>(dbContext as RahkaranDbContext, loggerFactory)
    where TEntity : BaseModel
{
    protected abstract DbSet<TEntity> EntityDbSet { get; }

    protected abstract string EntityName { get; }

    public Task<string[]> GetAllIdsAsync()
    {
        return EntityDbSet.Select(i => i.Id.ToString()).ToArrayAsync();
    }

    public async Task<string> GetMaxRowVersionAsync()
    {
        var version = await EntityDbSet
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
        var deleted = await _context.GetSingleValueAsync<long?>(MaxDeletedIdQuery, new SqlParameter("@EntityName", EntityName));

        return $"{deleted ?? 0}|{version}";
    }

    public Task<long[]> GetLastChangesAsync(byte[] timestamp)
    {
        return EntityDbSet
            .Where(x => StructuralComparisons.StructuralComparer.Compare(x.Version, timestamp) > 0)
            .Select(x => x.Id)
            .ToArrayAsync();
    }

    public async Task<string[]> GetLastDeletedIdsAsync(long lastSeen)
    {
        var result = await _context.GetListAsAsync<DeleteChange>(LastDeletedIdQuery,
            new SqlParameter("@EntityName", EntityName),
            new SqlParameter("@LastID", lastSeen));
        return [.. result.SelectMany(x => x.RecordIDs.Split(',')).Select(x => x.Trim())];
    }

    protected const string LastDeletedIdQuery = @"
SELECT ID, SUBSTRING(DetailSummary, DetailSummaryStart, IIF(DetailSummaryEnd > 0, DetailSummaryEnd, DetailSummaryLen) - DetailSummaryStart + 1) AS RecordIDs
FROM (
	SELECT ID, DetailSummary, LEN(DetailSummary) AS DetailSummaryLen, DetailSummaryStart
		, CHARINDEX(' - ', DetailSummary, DetailSummaryStart) as DetailSummaryEnd
	FROM (
		SELECT ID, DetailSummary, 1 + CHARINDEX(' ', DetailSummary, IIF(RecordID > 0, RecordID, RecordIDs)) AS DetailSummaryStart
		FROM (
			SELECT ID, DetailSummary
				, CHARINDEX('ID:', DetailSummary) AS RecordID
				, CHARINDEX('IDs:', DetailSummary) AS RecordIDs
			FROM [SYS3].[SecurityEvent]
			WHERE [Subject] = 22 AND [DetailSummary] LIKE @EntityName AND ID > @LastID
		) E
	) E
) E
";

    protected const string MaxDeletedIdQuery = @"
SELECT MAX(ID) AS ID
FROM [SYS3].[SecurityEvent]
WHERE [Subject] = 22 AND [DetailSummary] LIKE @EntityName
";
}
