const key: string = 'auth-tokens';
const device_id_key: string = 'device-id';
export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
}

export interface User {
  id: number;
  employeeId: number;
  companyId?: number;
  roles: RoleName[];
  email: string;
  name: string;
  isCompanyAdmin: boolean;
  isSuperAdmin: boolean;
  isEmployee: boolean;
}

export interface Credentials {
  email: string;
  password: string;
}

export function saveAuthTokens(tokens: AuthTokens) {
  localStorage.setItem(key, JSON.stringify(tokens));
}

export function deleteAauthTokens() {
  localStorage.removeItem(key);
}

export function getAuthTokens(): AuthTokens | undefined {
  return localStorage.getItem(key) ? JSON.parse(localStorage.getItem(key)!) : undefined;
}

export function getDeviceId(): string {
  let deviceId = localStorage.getItem(device_id_key);
  if (!deviceId) {
    {
      deviceId = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        const r = (Math.random() * 16) | 0,
          v = c == 'x' ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      });
      localStorage.setItem(device_id_key, deviceId);
    }
  }

  return localStorage.getItem(device_id_key)!;
}

export type RoleName = 'SuperAdmin' | 'CompanyAdmin' | 'Employee';
