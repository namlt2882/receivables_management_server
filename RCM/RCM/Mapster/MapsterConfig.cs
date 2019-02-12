using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;

namespace RCM.Mapster
{
    public class MapsterConfig
    {


        public MapsterConfig()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

        }
        public void Run()
        {
            
            //TypeAdapterConfig<HsGroupUser, UserViewModel>.NewConfig()
            //                    .Map(dest => dest.Id, src => src.UserId)
            //                    .Map(dest => dest.UserName, src => src.User.UserName)
            //                    .Map(dest => dest.FullName, src => src.User.FullName);

            //TypeAdapterConfig<HsGroupUser, GroupViewModel>.NewConfig()
            //                    .Map(dest => dest.Id, src => src.GroupId)
            //                    .Map(dest => dest.Name, src => src.Group.Name);

            //TypeAdapterConfig<UserCreateModel , HsUser>.NewConfig()
            //                    .Map(dest => dest.UserName, src => src.UserName);
        }
    }

}
