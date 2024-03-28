using AutoMapper;
using StorageSystem.Api;
using StorageSystem.Shared;
using StorageSystem.Shared.Context;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Services
{
    public class StorageOutDetailService : IStorageOutDetailService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public StorageOutDetailService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(StorageOutDetailDto model)
        {
            try
            {
                var storageOutDetail = mapper.Map<StorageOutDetail>(model);
                await work.GetRepository<StorageOutDetail>().InsertAsync(storageOutDetail);
                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, storageOutDetail);
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
                var repository = work.GetRepository<StorageOutDetail>();
                var storageOutDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(storageOutDetail);
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
                var repository = work.GetRepository<StorageOutDetail>();
                var storageOutDetails = await repository.GetPagedListAsync(predicate:
                    x => string.IsNullOrWhiteSpace(query.Search) ? true : x.WbsNo.Equals(query.Search)
                    || x.StorageName.Equals(query.Search) || x.MaterielSN.Equals(query.Search) || x.InStorageDate.ToString().Equals(query.Search),
                    pageIndex: query.PageIndex,
                    pageSize: query.PageSize);
                if (storageOutDetails.TotalCount > 0)
                {
                    return new ApiResponse(true, storageOutDetails);
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
                var repository = work.GetRepository<StorageOutDetail>();
                var storageOutDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.MaterielSN.Equals(sn));
                if (storageOutDetail != null)
                {
                    return new ApiResponse(true, storageOutDetail);
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
                var repository = work.GetRepository<StorageOutDetail>();
                var storageOutDetail = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (storageOutDetail != null)
                {
                    return new ApiResponse(true, storageOutDetail);
                }
                return new ApiResponse(false, "没有查到相关数据！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(StorageOutDetailDto model)
        {
            try
            {
                var repository = work.GetRepository<StorageOutDetail>();
                var storageOutDetail = mapper.Map<StorageOutDetail>(model);
                var storageOutDetailUpdate = await repository.GetFirstOrDefaultAsync(predicate: x => x.MaterielSN.Equals(model.MaterielSN));
                if (storageOutDetailUpdate != null)
                {
                    storageOutDetailUpdate.WbsNo = storageOutDetail.WbsNo;
                    storageOutDetailUpdate.StorageName = storageOutDetail.StorageName;
                    storageOutDetailUpdate.InStorageDate = storageOutDetail.InStorageDate;
                    storageOutDetailUpdate.MaterielDetail = model.MaterielDetail;
                    storageOutDetailUpdate.InFinish = storageOutDetail.InFinish;
                    storageOutDetailUpdate.OutStorageDate = storageOutDetail.OutStorageDate;
                    storageOutDetailUpdate.OutFinish = storageOutDetail.OutFinish;
                    repository.Update(storageOutDetailUpdate);

                    if (await work.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse(true, storageOutDetailUpdate);
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
