using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;
using System.Collections.Generic;

namespace QTD2.Test.Data.Infrastructure.Model.Procedure_IssuingAuthority
{
    public class Procedure_IssuingAuthorityCreateOptionsTestData
    {
        public static List<Procedure_IssuingAuthorityCreateOptions> GetAll()
        {
            return new List<Procedure_IssuingAuthorityCreateOptions>()
            {
                Null(),
                Empty(),
                PriorityIA(),
                NonPriorityIA(),
                NameTooLong()
            };
        }

        public static Procedure_IssuingAuthorityCreateOptions Null()
        {
            return null;
        }

        public static Procedure_IssuingAuthorityCreateOptions Empty()
        {
            return new Procedure_IssuingAuthorityCreateOptions();
        }

        public static Procedure_IssuingAuthorityCreateOptions PriorityIA()
        {
            return new Procedure_IssuingAuthorityCreateOptions()
            {
                Title = "Test Title 1",
                EffectiveDate = System.DateTime.Now,
                Website = "Test Website 1",
                IsDeleted = false,
                IsActive = true,
                Notes = "Test Notes 1",
                Description = "Priority IA"
            };
        }

        public static Procedure_IssuingAuthorityCreateOptions NonPriorityIA()
        {
            return new Procedure_IssuingAuthorityCreateOptions()
            {
                Title = "Test Title 2",
                EffectiveDate = System.DateTime.Now,
                Website = "Test Website 2",
                IsDeleted = false,
                IsActive = true,
                Notes = "Test Notes 2",
                Description = "Priority IA"
            };
        }

        public static Procedure_IssuingAuthorityCreateOptions NameTooLong()
        {
            return new Procedure_IssuingAuthorityCreateOptions()
            {
                Title = "Test Title 3",
                EffectiveDate = System.DateTime.Now,
                Website = "Test Website 3",
                IsDeleted = false,
                IsActive = true,
                Notes = "Test Notes 3",
                Description = "NameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLongNameTooLong"
            };
        }
    }
}
