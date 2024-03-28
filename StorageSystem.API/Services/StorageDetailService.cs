using AutoMapper;
using StorageSystem.Api;
using StorageSystem.Shared;
using StorageSystem.Shared.Context;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Services
{
    public class StorageDetailService : IStorageDetailService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public StorageDetailService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(StorageDetailDto model)
        {
            try
            {
                var storageDetail = mapper.Map<StorageDetail>(model);
                await work.GetRepository<StorageDetail>().InsertAsync(storageDetail);
                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, storageDetail);
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
                var repository = work.GetRepository<StorageDetail>();
                var storageDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(storageDetail);
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
                var repository = work.GetRepository<StorageDetail>();
                var storageDetails = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(query.Search) ? true : x.WbsNo.Equals(query.Search)
                    || x.StorageName.Equals(query.Search) || x.MaterielSN.Equals(query.Search) || x.InStorageDate.ToString().Equals(query.Search),
                    pageIndex: query.PageIndex,
                    pageSize: query.PageSize);
                if (storageDetails.TotalCount > 0)
                {
                    return new ApiResponse(true, storageDetails);
                }
                return new ApiResponse(false, "没有查到相关数据！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAsync(string sn)
        {
            try
            {
                var repository = work.GetRepository<StorageDetail>();
                var storageDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.MaterielSN.Equals(sn));
                if (storageDetail != null)
                {
                    return new ApiResponse(true, storageDetail);
                }
                return new ApiResponse(false, "没有查到相关数据！");
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
                var repository = work.GetRepository<StorageDetail>();
                var storageDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (storageDetail != null)
                {
                    return new ApiResponse(true, storageDetail);
                }
                return new ApiResponse(false, "没有查到相关数据！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(StorageDetailDto model)
        {
            try
            {
                var repository = work.GetRepository<StorageDetail>();
                var storageDetail = mapper.Map<StorageDetail>(model);
                var storageDetailUpdate = await repository.GetFirstOrDefaultAsync(predicate: x => x.MaterielSN.Equals(model.MaterielSN));
                if (storageDetailUpdate != null)
                {
                    storageDetailUpdate.WbsNo = storageDetail.WbsNo;
                    storageDetailUpdate.StorageName = storageDetail.StorageName;
                    storageDetailUpdate.InStorageDate = storageDetail.InStorageDate;
                    storageDetailUpdate.MaterielDetail = model.MaterielDetail;
                    storageDetailUpdate.InFinish = storageDetail.InFinish;
                    repository.Update(storageDetailUpdate);

                    if (await work.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse(true, storageDetailUpdate);
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
