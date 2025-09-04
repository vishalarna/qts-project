using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class ProcedureTestData
    {
        public static List<Procedure> GetAll()
        {
            return new List<Procedure>()
            {
                Proc1(),Proc2()
            };
        }

        static Procedure Proc1()
        {
            byte[] smallArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var proc = new Procedure(1, "1", "title", "New Procedure for a Task", "", DateOnly.FromDateTime(DateTime.Now), smallArray, true, true, "", "FileName");
            proc.Set_Id(1);
            return proc;
        }

        static Procedure Proc2()
        {
            byte[] smallArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var proc = new Procedure(2, "1", "title", "New Procedure for a Task", "", DateOnly.FromDateTime(DateTime.Now), smallArray,  true, true, "", "FileName");
            proc.Set_Id(2);
            return proc;
        }
    }
}
