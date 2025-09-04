var read = Console.ReadLine();

if(read == "UpdateHistoricScorms")
{
    UpdateHistoricScorms();
}

void UpdateHistoricScorms()
{
    //foreach database in Instances where active = 1
    //  obtain a list of CBT_ScormRegsitrations
    //  foreach cbtScormRegistration in cbtScormRegistrations
    //      call the new method descrbed in the ticket for scorm http client "registrations/{cbtScormRegistration.cbtScormUploadId+'.'+cbtScormRegistration.classScheduleEmployeeId}"
    //      if it returns successfully 
    //          run SaveResponses for the cbtScormRegistration with the resulting runtimeInteractions
}
