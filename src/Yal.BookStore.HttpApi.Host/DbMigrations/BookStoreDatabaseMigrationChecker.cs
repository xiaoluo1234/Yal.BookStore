using System;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Yal.BookStore.EntityFrameworkCore;

namespace Yal.BookStore.HttiApi.Host.DbMigrations
{
    public class BookStoreDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<BookStoreDbContext>
    {
        public BookStoreDatabaseMigrationChecker(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IAbpDistributedLock abpDistributedLock)
            : base(
                unitOfWorkManager,
                serviceProvider,
                currentTenant,
                distributedEventBus,
                abpDistributedLock,
                "Default")
        {
        }
    }
}
