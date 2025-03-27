export interface IPermissionService {
  isUnauthenticated(): boolean;
  getAccessToken(): string;
  checkAdminPermission(): boolean;
}
