using AutoMapper;
using StorageSystem.Api;
using StorageSystem.Shared;
using StorageSystem.Shared.Context;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Services
{
    public class StorageStatusService : IStorageStatusService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public StorageStatusService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(StorageStatusDto storageStatusDto)
        {
            try
            {
                var dbStorStatus = mapper.Map<StorageStatus>(storageStatusDto);
                var repository = work.GetRepository<StorageStatus>();
                var status = await repository.GetFirstOrDefaultAsync(predicate: x => x.StorageName.Equals(dbStorStatus.StorageName));
                if (status == null)
                {
                    await repository.InsertAsync(dbStorStatus);
                    if (await work.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse(true, dbStorStatus);
                    }

                    return new ApiResponse("添加数据失败！");
                }
                return new ApiResponse("添加数据失败！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<StorageStatus>();
                var storageStatus = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(storageStatus);
                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = work.GetRepository<StorageStatus>();
                var storageStatuses = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(query.Search) ? true : x.StorageName.Equals(query.Search),
                    pageIndex: query.PageIndex,
                    pageSize: query.PageSize);
                if (storageStatuses.TotalCount > 0)
                {
                    return new ApiResponse(true, storageStatuses);
                }
                return new ApiResponse(false, "没有查到相关数据");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAsync(string storageName)
        {
            try
            {
                var repository = work.GetRepository<StorageStatus>();
                var storageStatus = await repository.GetFirstOrDefaultAsync(predicate: x => x.StorageName.Equals(storageName));
                if (storageStatus != null)
                {
                    return new ApiResponse(true, storageStatus);
                }
                return new ApiResponse(false, "没有查到相关数据");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<StorageStatus>();
                var storageStatus = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (storageStatus != null)
                {
                    return new ApiResponse(true, storageStatus);
                }
                return new ApiResponse(false, "没有查到相关数据");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetGruopAsync(string groupName)
        {
            try
            {
                var repository = work.GetRepository<StorageStatus>();
                var storageStatuses = await repository.GetAllAsync(predicate: x => x.StorageName.Contains(groupName));
                if (storageStatuses.Count > 0)
                {
                    return new ApiResponse(true, storageStatuses);
                }
                return new ApiResponse(false, "没有查到相关数据");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(StorageStatusDto storageStatusDto)
        {
            try
            {
                var dbStorStatus = mapper.Map<StorageStatus>(storageStatusDto);
                var repository = work.GetRepository<StorageStatus>();
                var status = await repository.GetFirstOrDefaultAsync(predicate: x => x.StorageName.Equals(dbStorStatus.StorageName));
                if (status != null)
                {
                    status!.Status = dbStorStatus.Status;

                    repository.Update(status);

                    if (await work.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse(true, status);
                    }

                    return new ApiResponse("更新数据异常！");
                }
                return new ApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
