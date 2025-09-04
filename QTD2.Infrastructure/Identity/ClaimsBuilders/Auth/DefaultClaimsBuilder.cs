using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.ClaimsBuilders
{
    public class DefaultClaimsBuilder : IClaimsBuilder
    {
        Database.Interfaces.IDbContextBuilder _contextBuilder;
        Domain.Interfaces.Service.Authentication.IInstanceSettingService _instanceSettingsService;
        Domain.Interfaces.Service.Authentication.IInstanceService _instanceService;

        Metrics.Interfaces.IMetricLogger _metricLogger;

        public DefaultClaimsBuilder(
                Database.Interfaces.IDbContextBuilder contextBuilder,
                Domain.Interfaces.Service.Authentication.IInstanceSettingService instanceSettingsService,
                Domain.Interfaces.Service.Authentication.IInstanceService instanceService,
                 Metrics.Interfaces.IMetricLogger metricLogger)
        {
            _contextBuilder = contextBuilder;
            _instanceSettingsService = instanceSettingsService;
            _instanceService = instanceService;
            _metricLogger = metricLogger;
        }

        public List<Claim> Build(AppUser user, List<Claim> claims, ClaimsBuilderOptions options)
        {
            List<Claim> nClaims = new List<Claim>();

            nClaims.Add(new Claim(CustomClaimTypes.UserName, user.UserName));
            nClaims.Add(new Claim(ClaimTypes.Name, user.UserName));

            if (!options.Is2FAApproved)
            {
                nClaims.Add(new Claim(CustomClaimTypes.TfaRequired, "true"));
                return nClaims;
            }

            var adminDbClaim = claims.Where(r => r.Type == CustomClaimTypes.IsAdmin).FirstOrDefault();


            if (adminDbClaim != null)
            {
                nClaims.Add(adminDbClaim);
            }

            bool isAdmin = adminDbClaim == null ? false : Convert.ToBoolean(adminDbClaim.Value);

            var instanceNames = claims.Where(r => r.Type == CustomClaimTypes.InstanceName).Select(c => c.Value).ToList();

            List<Instance> instances = new List<Instance>();

            if (instanceNames != null && instanceNames.Count() > 0)
                instances = _instanceService.GetInstancesAndClientsByNamesAsync(instanceNames).Result;

            // Limit to only Instances where the Client and the Instance are Active
            instances = instances.Where(i => i.Active && i.Client.Active).ToList();

            if (instances.Count() == 0)
            {
                nClaims.Add(new Claim(CustomClaimTypes.HasZeroInstance, true.ToString()));
                nClaims.Add(new Claim(CustomClaimTypes.HasMultipleInstances, false.ToString()));
            }
            else if (instances.Count() > 1)
            {
                nClaims.Add(new Claim(CustomClaimTypes.HasZeroInstance, false.ToString()));
                nClaims.Add(new Claim(CustomClaimTypes.HasMultipleInstances, true.ToString()));
            }
            else
            {
                nClaims.Add(new Claim(CustomClaimTypes.HasZeroInstance, false.ToString()));
                nClaims.Add(new Claim(CustomClaimTypes.HasMultipleInstances, false.ToString()));
            }

            var selectedInstanceClaim = claims.Where(r => r.Type == CustomClaimTypes.InstanceName && r.Value == options.SelectInstance && instances.Select(i => i.Name.ToUpper()).ToList().Contains(options.SelectInstance.ToUpper())).FirstOrDefault();
            if (!string.IsNullOrEmpty(options.SelectInstance) && selectedInstanceClaim != null)
            {
                var qtdClaims = buildQtdClaims(options.SelectInstance, user.UserName);
                foreach (var claim in qtdClaims)
                {
                    nClaims.Add(claim);
                }
                nClaims.Add(selectedInstanceClaim);

                var instance = instances.FirstOrDefault(i => i.Name.ToUpper() == options.SelectInstance.ToUpper());
                nClaims.Add(new Claim(CustomClaimTypes.IsBetaInstance, instance.IsInBeta.ToString()));
            }
            else if (!string.IsNullOrEmpty(options.SelectInstance) && isAdmin)
            {
                if (!string.IsNullOrEmpty(options.ImpersonateUser))
                {
                    //set imp user
                    //validate imp user claim

                    nClaims.Add(new Claim(CustomClaimTypes.InstanceName, options.SelectInstance));

                    var instance = instances.FirstOrDefault(i => i.Name.ToUpper() == options.SelectInstance.ToUpper());
                    nClaims.Add(new Claim(CustomClaimTypes.IsBetaInstance, instance.IsInBeta.ToString()));
                }
                else
                {
                    throw new UnauthorizedAccessException("You must select a user to impersonate to do this");
                }
            }
            else if (!string.IsNullOrEmpty(options.SelectInstance) && selectedInstanceClaim == null)
            {
                throw new UnauthorizedAccessException("You do not have access to this instance");
            }
            else if (instances.Count() == 1 && !isAdmin)
            {
                // Get claims for the auto-instance selection about to occur
                var instance = instances.First();
                var instanceClaim = claims.First(r => r.Type == CustomClaimTypes.InstanceName && r.Value.ToUpper() == instance.Name.ToUpper());
                nClaims.Add(instanceClaim);

                _metricLogger.AddSelectInstanceEvent(user.Id, instanceClaim.Value).Wait();

                var qtdClaims = buildQtdClaims(instanceClaim.Value, user.UserName);
                foreach (var claim in qtdClaims)
                {
                    nClaims.Add(claim);
                }

                nClaims.Add(new Claim(CustomClaimTypes.IsBetaInstance, instance.IsInBeta.ToString()));
            }

            return nClaims;
        }


        protected List<Claim> buildQtdClaims(string instanceName, string username)
        {
            List<Claim> claims = new List<Claim>();

            var instance = _instanceSettingsService.GetByInstanceNameAsync(instanceName).Result;
            var context = _contextBuilder.BuildQtdContext(instance.DatabaseName);

            // get QTDUser
            var qtdUser = context.QTDUsers.Where(r => r.Person.Username == username && !r.Deleted && r.Active).FirstOrDefault();
            claims.Add(new Claim(CustomClaimTypes.IsQTDUser, (qtdUser != null).ToString()));

            if (qtdUser != null)
            {
                claims.Add(new Claim(CustomClaimTypes.QTDUserId, qtdUser.Id.ToString()));
            }

            // get Employee
            var employee = context.Employees.Where(r => r.Person.Username == username && !r.Deleted && r.Active && !r.Person.Deleted).FirstOrDefault();

            claims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, (employee != null).ToString()));

            if (employee != null)
            {
                claims.Add(new Claim(CustomClaimTypes.EmployeeId, employee.Id.ToString()));
            }

            // get ClientUser
            var clientUser = context.ClientUsers.Where(r => r.Person.Username == username && !r.Deleted && r.Active && !r.Person.Deleted).FirstOrDefault();

            claims.Add(new Claim(CustomClaimTypes.IsClientUser, (clientUser != null).ToString()));

            if (clientUser != null)
            {
                claims.Add(new Claim(CustomClaimTypes.ClientUserId, clientUser.Id.ToString()));
            }

            // get Admin User
            //var adminUser = context.Where(r => r.Person.Username == username).FirstOrDefault();

            //claims.Add(new Claim(CustomClaimTypes.IsClientUser, (clientUser != null).ToString()));

            //if (clientUser != null)
            //{
            //    claims.Add(new Claim(CustomClaimTypes.ClientUserId, clientUser.Id.ToString()));
            //}

            // get Instructor 
            var instructor = context.Instructors.Where(r => r.InstructorEmail == username && r.Active).FirstOrDefault();

            claims.Add(new Claim(CustomClaimTypes.isInstructor, (instructor != null).ToString()));

            if (instructor != null)
            {
                claims.Add(new Claim(CustomClaimTypes.InstructorId, instructor.Id.ToString()));
            }


            return claims;
        }
    }
}
