export abstract class CustomClaimTypes {
  //todo add domain
  static _domain: string = 'qtd';
  public static readonly _prefix: string =
    CustomClaimTypes._domain + '/claims/';

  public static get UserName() {
    return CustomClaimTypes._prefix + '/username';
  }
  public static get IsAdmin() {
    return CustomClaimTypes._prefix + '/isAdmin';
  }
  public static get IsManager() {
    return CustomClaimTypes._prefix + '/isManager';
  }
  public static get IsInstructor() {
    return CustomClaimTypes._prefix + '/isInstructor';
  }
  public static get ClientName() {
    return CustomClaimTypes._prefix + '/clientName';
  }
  public static get IsClientAdmin() {
    return CustomClaimTypes._prefix + '/isClientAdmin';
  }
  public static get ClientUserName() {
    return CustomClaimTypes._prefix + '/clientUserName';
  }
  public static get TfaRequired() {
    return CustomClaimTypes._prefix + '/2faRequired';
  }
  public static get Revoked() {
    return CustomClaimTypes._prefix + '/revoked';
  }
  public static get InstanceName() {
    return CustomClaimTypes._prefix + '/instanceName';
  }
  public static get ClientAdmin() {
    return CustomClaimTypes._prefix + '/clientAdmin';
  }
  public static get IsEmployee() {
    return CustomClaimTypes._prefix + '/isEmployee';
  }
  public static get IsQtdUser() {
    return CustomClaimTypes._prefix + '/isQtdUser';
  }
  public static get HasMultipleInstances() {
    return CustomClaimTypes._prefix + '/hasMultipleInstances';
  }
  public static get IsBetaInstance() {
    return CustomClaimTypes._prefix + '/isBetaInstance';
  }
  public static get HasZeroInstances() {
    return CustomClaimTypes._prefix + '/hasZeroInstance';
  }
}
