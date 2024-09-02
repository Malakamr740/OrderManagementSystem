//using Mapster;
//using OrderBase.Entities;

//namespace OrderBase.Mapping
//{
//    public class MyMapsterConfiguartion 
//    {
//        public static void Configure()
//        {
//            TypeAdapterConfig.GlobalSettings.ForType<Product>()
//             .Map(src => src.Amount, dest => dest.Amount)
//             .Map(src => src.Name, dest => dest.Name)
//             .Map(src => src.Status, dest => dest.Status)
//             .Map(src => src.Description, dest => dest.Description)
//             .Map(src => src.Type, dest => dest.Type)
//             .Ignore(src => src.Id); 
//        }
//    }
//}
