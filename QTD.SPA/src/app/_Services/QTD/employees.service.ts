import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map, takeUntil } from 'rxjs/operators';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeCertification } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertification';
import { EmployeeCertificateCreateOptions } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertificateCreateOptions';
import { EmployeeCertificateUpdateOptions } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertificateUpdateOptions';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { EmployeePositionCreateOptions } from 'src/app/_DtoModels/EmployeePosition/EmployeePositionCreateOptions';
import { EmployeePositionUpdateOptions } from 'src/app/_DtoModels/EmployeePosition/EmployeePositionUpdateOptions';
import { EmployeeUpdateOptions } from 'src/app/_DtoModels/Employee/EmployeeUpdateOptions';
import { EmployeeCreateOptions } from 'src/app/_DtoModels/Employee/EmployeeCreateOptions';
import { EmployeeOrganizationUpdateOptions } from 'src/app/_DtoModels/EmployeeOrganization/EmployeeOrganizationUpdateOptions';
import { EmployeeOrganization } from 'src/app/_DtoModels/EmployeeOrganization/EmployeeOrganization';
import { EmployeeOrganizationCreateOptions } from 'src/app/_DtoModels/EmployeeOrganization/EmployeeOrganizationCreateOptions';
import { EmployeeOptions } from 'src/app/_DtoModels/Employee/EmployeeOptions';
import { EmployeeDocumentOptions } from 'src/app/_DtoModels/Employee/EmployeeDocumentOptions';
import { EmployeeLinkCertification } from 'src/app/_DtoModels/Employee/EmployeeLinkCertification';
import { EmpCertificateUpdateOptions } from 'src/app/_DtoModels/Employee/EmpCertificateUpdateOptions';
import { EmployeeWithPositionVM } from 'src/app/_DtoModels/Employee/EmployeeWithPositionVM';
import { EmpTestCreateOption } from 'src/app/_DtoModels/Employee/EmpTestCreateOption';
import { EmployeeSummary } from 'src/app/_DtoModels/Employee/EmployeeSummary';
import { EMPCertificationVM } from 'src/app/_DtoModels/EmployeeCertification/EMPCertificationVM';
import { IDPReviewUpdateOptions } from '../../_DtoModels/Employee/IDPReviewUpdateOptions'
import { EmployeeNameOnlyVM } from 'src/app/_DtoModels/Employee/EmployeeNameOnlyVM';
import { EmployeeCertificationHistory } from '@models/EmployeeCertificationHistory/EmployeeCertificationHistory';
import { EmployeeAnswerModel } from '@models/Test/EmployeeAnswerModel';
import { ReviewTestModel } from '@models/Test/ReviewTestModel';
import { SubmitTestModel } from '@models/Test/SubmitTestModel';
import { firstValueFrom } from 'rxjs';
import { EmployeeCertificationHistoryCreateOptions } from '@models/EmployeeCertificationHistory/EmployeeCertificationHistoryCreateOptions';
import { EmployeeCertificationHistoryDeleteOptions } from '@models/EmployeeCertificationHistory/EmployeeCertificationHistoryDeleteOptions';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  baseUrl = environment.QTD + 'employees';
  constructor(private http: HttpClient) { }

  //#region Employees
  getAll() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['employees'] as any[];
      })
    ));
  }

  getAllSimplifiedEmployees() {
    return firstValueFrom(this.http.get(this.baseUrl + '/simplifiedlist').pipe(
      map((res: EmployeeSummary) => {
        return res['result'] as EmployeeSummary[];
      })
    ));
  }

  getAllEmployees() {
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res: any) => {
        return res['employees'] as Employee[];
      })
    ));
  }

  getEmployeeList(){
    return firstValueFrom(this.http.get(this.baseUrl+'/pos/org').pipe(
      map((res:any)=>{
        return res['result'] as Employee[];
      })
    ));
  }


  getEmployeeListNamesOnly(){
    return firstValueFrom(this.http.get(this.baseUrl+'/names/list').pipe(
      map((res:any)=>{
        return res['result'] as EmployeeNameOnlyVM[];
      })
    ));
  }

  getEmployeeWithOrgAndPosList(){
    return firstValueFrom(this.http.get(this.baseUrl+'/organization/position/list').pipe(
      map((res:any)=>{
        return res['result'] as any[];
      })
    ));
  }
  

  getEmployeesExpiredCertifications() {
    return firstValueFrom(this.http.get(this.baseUrl+'/expiredCertifications').pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ));
  }

  getAllActiveEmpForEnroll() {
    return firstValueFrom(this.http.get(this.baseUrl + '/active/forEnroll').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getEmployeesStatistics() {
    return firstValueFrom(this.http.get(this.baseUrl + "/statistics")
    .pipe(
      map((res: any) => {
        return res.result;
      })
    ));
  }

  async get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['employee'] as Employee;
        })
      )
      );
  }

  create(option: EmployeeCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res['employees'] as Employee;
        })
      )
      );
  }

  update(id: any, option: EmployeeUpdateOptions) {
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`, option).pipe(
      map((res: any) => {

        return res['employee'] as Employee;
      })
    ));
  }

  delete(id: any, actionType: string) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/${actionType}`).pipe(
      map((res: any) => {

        return res.message;
      })
    ));
  }

  deleteEmployee(options: EmployeeOptions) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: options,
    };
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/parent`, httpOptions)
      .pipe(
        map((res: any) => {
          return res.message;
        })
      )
      );
  }

  //#endregion Employees

  //#region Employee Certifications

  async getCertifications(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/certifications`)
      .pipe(
        map((res: any) => {

          return res['employeeCertification'] as EmployeeCertification[];
        })
      )
      );
  }

  async getEmpCertificationHistory(empCertId:string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/certifications/history/${empCertId}`)
      .pipe(
        map((res: any) => {

          return res['result'] as EmployeeCertificationHistory[];
        })
      )
      );
  }

  createCertifications(
    id: any,
    option: EmployeeCertificateCreateOptions
  ) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/certifications`, option)
      .pipe(
        map((res: any) => {

          return res['employeeCertification'] as EmployeeCertification;
        })
      )
      );
  }

  updateCertifications(
    id: any,
    option: any
  ) {

    return firstValueFrom(this.http
      .put(
        this.baseUrl + `/empcertifications/${id}`,
        option
      )
      .pipe(
        map((res: any) => {

          return res['employeeCertification'] as EmployeeCertification;
        })
      ));
  }

  updateCertificationInHistory(id:any,option:any){
    return firstValueFrom(this.http.put(this.baseUrl + `/certifications/${id}/history`,option).pipe(
      map((res:any)=>{
        res['message'] as string;
      })
    ));
  }

  deleteCertifications(id: any) {
    return firstValueFrom(this.http
      .delete(
        this.baseUrl + `/certifications/${id}`
      )
      .pipe(
        map((res: any) => {

          return res.message;
        })
      ));
  }

  deleteCertificationsFromHist(certLinkId:any){
    return firstValueFrom(this.http
      .delete(
        this.baseUrl + `/certifications/${certLinkId}/history`
      )
      .pipe(
        map((res: any) => {

          return res.message;
        })
      ));
  }
  //#endregion Employee Certifications

  //#region Employee Positions

  getPositions(id: any, filter: any) {

    return firstValueFrom(this.http
      //.get(this.baseUrl + `/${id}/positions/${filter}`)
      .get(this.baseUrl + `/positions/${id}/${filter}`)
      //.get(this.baseUrl + `/linkedCertifications/${id}/${filter}`)
      .pipe(
        map((res: any) => {

          return res['employeePositions'] as EmployeePosition[];
        })
      )
      );
  }

  createPosition(employeeId: any, option: EmployeePositionCreateOptions) {

    return firstValueFrom(this.http
      .post(this.baseUrl + `/${employeeId}/positions`, option)
      .pipe(
        map((res: any) => {

          return res['employeePosition'] as EmployeePosition;
        })
      )
      );
  }

  updatePosition(
    id: any,
    positionid: any,
    option: EmployeePositionUpdateOptions
  ) {

    return firstValueFrom(this.http
      .put(
        encodeURI(this.baseUrl + `/${id}/positions/${positionid}`),
        option
      )
      .pipe(
        map((res: any) => {

          return res['employeePosition'] as EmployeePosition;
        })
      ));
  }

  deletePosition(id: any, positionid: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/positions/${positionid}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      ));
  }
  //#endregion Employee Positions

  //#region Employee Organization

  getOrganizations(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/organizations`)
      .pipe(
        map((res: any) => {

          return res['organizations'] as EmployeeOrganization[];
        })
      )
      );
  }

  createOrganization(
    id: any,
    option: EmployeeOrganizationCreateOptions
  ) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/organizations`, option)
      .pipe(
        map((res: any) => {

          return res['organization'] as EmployeeOrganization;
        })
      ));
  }

  updateOrganization(
    id: any,
    organizationid: any,
    option: EmployeeOrganizationUpdateOptions
  ) {

    return firstValueFrom(this.http
      .put(
        encodeURI(
          this.baseUrl + `/${id}/organizations/${organizationid}`
        ),
        option
      )
      .pipe(
        map((res: any) => {

          return res.message;
        })
      ));
  }

  deleteOrganization(id: any, organizationid: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/organizations/${organizationid}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      ));
  }

  LinkOrganizationtoEmployee(id: any, options: any) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/organizations`, options)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  GetLinkedOrganizationtoEmployee(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/organizations`)
      .pipe(
        map((res: any) => {
          return res['organizations'] as any;
        })
      )
      );
  }
  ToggleManagerStatus(id: any, options: any) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/organizations`, options)
      .pipe(
        map((res: any) => {
          return res['organizations'] as any;
        })
      )
      );
  }


  GetEmployeeLinkedToOrganization(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/organizations/${id}`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
  //#endregion Employee Organization

  uploadFile(empId: any, formData: any) {
    return firstValueFrom(this.http.put(this.baseUrl + `/upload/${empId}`, formData).pipe(
      map((res: any) => {
        return res['message']
      })
    ));
  }

  getUploadedFiles(empId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/getupload/${empId}`).pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ));
  }

  getDownloadData(id: any,fileId:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${fileId}/download/${id}`).pipe(
      map((res: any) => {
        return res['result'];
      })
    ));
  }

  createCertification(id: any, option: EmployeeLinkCertification) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/certifications`, option)
      .pipe(
        map((res: any) => {

          return res['employeeCertification'] as EmployeeCertification;
        })
      )
      );
  }

  getEmployeeCertifications(id: any, filter: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/linkedCertifications/${id}/${filter}`)
      .pipe(
        map((res: any) => {

          return res['result'] as EMPCertificationVM[];
        })
      )
      );
  }
  getPositionsByEmpAndPosId(employeeId: any, positionId: any,empPosId:any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${employeeId}/positions/${positionId}/${empPosId}`)
      .pipe(
        map((res: any) => {

          return res['employeePositions'] as EmployeePosition;
        })
      )
      );
  }

  updateLinkedPosition(id: any, positionid: any, option: EmployeePositionUpdateOptions) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}/positions/${positionid}`, option)
      .pipe(
        map((res: any) => {

          return res['employeePosition'] as EmployeePosition;
        })
      )
      );
  }

  deletempcertification(id: any, certId: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}/certifications/${certId}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  deletempposition(employeeId: any, positionId: any,empPosId:any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${employeeId}/positions/${positionId}/${empPosId}`)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  renewcertificate(id: any, options: EmpCertificateUpdateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/renew/empcertifications/${id}`, options)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }
  getEmployeeCertification(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/certifications`)
      .pipe(
        map((res: any) => {

          return res['employeeCertification'] as any;
        })
      )
      );

  }

  setCerificationStatus(empId: any, option: any) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${empId}/certifications`, option)
      .pipe(
        map((res: any) => {

          return res['message'] as any;
        })
      )
      );

  }

  getAllEvaluators() {
    return firstValueFrom(this.http.get(this.baseUrl + '/evals').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getAllEvaluatorsNamesOnly() {
    return firstValueFrom(this.http.get(this.baseUrl + '/evals/names').pipe(
      map((res: any) => {
        return res['result'] as any[];
      })
    ));
  }

  getEmpWithPosAndOrg() {
    return firstValueFrom(this.http.get(this.baseUrl + '/pos/org').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getEmpWithPosAndOrgIdsOnly(){
    return firstValueFrom(this.http.get(this.baseUrl + '/pos/org/ids').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getOnlyEmployees() {
    return firstValueFrom(this.http.get(this.baseUrl + '/onlyemp').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getTestEmployees() {
    return firstValueFrom(this.http.get(this.baseUrl + '/tests').pipe(
      map((res:any)=>{
        return res['employeeTest'] as Employee[];
      })
    ));
  }
  saveEmployeeTest(options: EmpTestCreateOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/tests`, options)
      .pipe(
        map((res: any) => {

          return res.message;
        })
      )
      );
  }

  getQuestionInfo(classId: any, testId: any, questionId: any,rosterId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/class/${classId}/roster/${rosterId}/test/${testId}/question/${questionId}`).pipe(
      map((res: any) => {

        return res['employeeTest'] as EmployeeAnswerModel;
      })
    ));
  }
  reviewTest(rosterId:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/class/roster/${rosterId}`).pipe(
      map((res: any) => {

        return res['employeeTest'] as ReviewTestModel[];
      })
    ));
  }

  submitTest(classId: any, testId: any,rosterId:any,completionDate:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/class/${classId}/roster/${rosterId}/submittest/${testId}/compDate/${completionDate}`).pipe(
      map((res: any) => {

        return res['employeeTest'] as SubmitTestModel[];
      })
    ));
  }

  getEmployeeWithPosition(id: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/withPos`).pipe(
      map((res: any) => {
        return res['result'] as EmployeeWithPositionVM;
      })
    ));
  }

  exitTest(rosterId:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/class/exit/roster/${rosterId}`).pipe(
      map((res: any) => {

        return res['employeeTest'] as any;
      })
    ));
  }

  getAvailableCourses(currentDate:any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg/available/currentDateTime/${currentDate}`).pipe(
      map((res: any) => {
        return res['result'] as any;
      })
    ));
  }
  getApprovedCourses() {
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg/approved`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }
  getDeniedCourses() {
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg/denied`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }
  getDroppedCourses() {
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg/dropped`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }
  registerAvailableCourses(classId: any, ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/selfreg/${classId}/ila/${ilaId}`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }
  dropCourses(classId: any, ilaId: any) {

    return firstValueFrom(this.http.put(this.baseUrl + `/drop/${classId}/ila/${ilaId}`, null).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }

  getClassDetail(classId: any, ilaId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/class/${classId}/ila/${ilaId}`).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }
  joinWaitList(classId: any, ilaId: any) {
    return firstValueFrom(this.http.put(this.baseUrl + `/joiWaitlist/${classId}/ila/${ilaId}`, null).pipe(
      map((res: any) => {

        return res['result'] as any;
      })
    ));
  }

  getAllEmployeeList(name:string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/list/${name}`).pipe(
      map((res: EmployeeSummary) => {
        return res['result'] as any;
      })
    ));
  }

  getImageWithUserName(userName:string){
    return firstValueFrom(this.http.get(this.baseUrl + `/${userName}/image`).pipe(
      map((res:any)=>{
        return res['result'] as any;
      })
    ));
  }

  getUserName(){
    return firstValueFrom(this.http.get(this.baseUrl + `/username`).pipe(
      map((res:any)=>{
        return res['result'] as string;
      })
    ));
  }

  getEmployeeCertificationFromHistory(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/certifications/${id}/history`).pipe(
      map((res:any)=>{
        return res['result'] as EmployeeCertification;
      })
    ));
  }

  updateIDPInfo(id:any,options:IDPReviewUpdateOptions){
    return firstValueFrom(this.http.put(this.baseUrl  + `/${id}/idpreview`,options).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  getAllEmployeesList(){
    return firstValueFrom(this.http.get(this.baseUrl + '/list').pipe(
      map((res: any) => {
        return res['result'] as Employee[];
      })
    ));
  }

  getEmployeeByClassScheduleIdAsync(cseId:string) {
    return firstValueFrom(this.http.get(this.baseUrl + `/ClassScheduleEmployee/${cseId}`).pipe(
      map((res: any) => {
        return res.result;
      })
    ));
  }

  async getEmployeeCertificationsByEmployeeId(empId: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${empId}/allempcertifications`)
      .pipe(
        map((res: any) => {

          return res['employeeCertifications'] as EmployeeCertification[];
        })
      )
      );
  }

  getEmployeeByUserNameAsync(username: string){
      return firstValueFrom(this.http.get(this.baseUrl + `/getByUserName/${username}`).pipe(
        map((res: any) => {
          return res['employee'] as Employee;
        })
      ));
    }

  createCertificationsHistory(option: EmployeeCertificationHistoryCreateOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/history`, option)
      .pipe(
        map((res: any) => {
               return res['result'] as EmployeeCertificationHistoryCreateOptions;
        })
      ));
  }

  bulkdeleteCertificationsFromHistoryAsync(options: EmployeeCertificationHistoryDeleteOptions){
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/certifications/history/bulk`, {
        body: options
      })
       .pipe(
        map((res: any) => {
          return res.message;
        })
      ));
  }

}
