export interface IPermissionService {
  isUnauthenticated(): boolean;
  getAccessToken(): string;
  checkAdminOrEmployeePermission(): boolean;
}
