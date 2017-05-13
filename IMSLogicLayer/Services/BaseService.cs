using IMSDBLayer.DataAccessInterfaces;

using IMSDBLayer.DataAccessObjects;

using IMSLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSLogicLayer.Services
{
    public class BaseService
    {
        private IClientDataAccess clientDataAccess;
        private IDistrictDataAccess districtDataAccess;
        private IInterventionDataAccess interventionDataAccess;
        private IInterventionTypeDataAccess interventionTypeDataAccess;
        private IUserDataAccess userDataAccess;
        private IReportDataAccess reportDataAccess;
        /// <summary>
        /// Initialise data access from connection string
        /// </summary>
        /// <param name="connstring">connection string to the database</param>
        public BaseService()
        {
            this.clientDataAccess = new ClientDataAccess();
            this.districtDataAccess = new DistrictDataAccess();
            this.interventionDataAccess = new InterventionDataAccess();
            this.interventionTypeDataAccess = new InterventionTypeDataAccess();
            this.userDataAccess = new UserDataAccess();
            this.reportDataAccess = new ReportDataAccess();
        }

        public IReportDataAccess ReportDataAccess { get => reportDataAccess; set => reportDataAccess = value; }

        internal IClientDataAccess Clients
        {
            get { return clientDataAccess; }
            set { clientDataAccess = value; }
        }

        internal IDistrictDataAccess Districts
        {
            get { return districtDataAccess; }
            set { districtDataAccess = value; }
        }

        internal IInterventionDataAccess Interventions
        {
            get { return interventionDataAccess; }
            set { interventionDataAccess = value; }
        }

        internal IInterventionTypeDataAccess InterventionTypes
        {
            get { return interventionTypeDataAccess; }
            set { interventionTypeDataAccess = value; }
        }
     
        internal IUserDataAccess Users
        {
            get { return userDataAccess; }
            set { userDataAccess = value; }
        }
      
        /// <summary>
        /// Get All intervention types
        /// </summary>
        /// <returns>A list of intervention types</returns>
        internal List<InterventionType> GetInterventionTypes()
        {
            return InterventionTypes.getAll().Select(i => new InterventionType(i)).ToList();
        }
        /// <summary>
        /// Get the current user from the identity id
        /// </summary>
        /// <param name="identityId">The identityid of the current logged in user</param>
        /// <returns>An user instance</returns>
        internal User GetDetail(Guid identityId)
        {
            User user = new User(Users.fetchUserByIdentityId(identityId));
            user.District = new District(Districts.fetchDistrictById(user.DistrictId.Value));
            return user;
        }
    }
}
