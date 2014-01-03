using Viiar.AtmFinder.W8.DataSources;
using Viiar.AtmFinder.W8.OnlineServices;

namespace Viiar.AtmFinder.W8
{
    public static class EntityExtensions
    {
        public static EntityDataItem ConvertToDataItem(this Entity entity, EntityDataGroup group)
        {
            var result = new EntityDataItem(
                    entity.Id.ToString(), 
                    entity.Title, 
                    entity.DistanceView, 
                    entity.Chain, 
                    entity.Country, 
                    entity.Address, 
                    entity.Description, 
                    entity.Latitude, 
                    entity.Longitude, 
                    group)
                             {
                                     CommissionWithdrawal = entity.ShowCommissionWithdrawalSign,
                                     CashDirection = entity.CashDirection,
                                     EntityType = entity.EntityType
                             };

            return result;
        }
    }
}
