import { User, getAuthTokens } from '../types';

export function parseToken() {
  const tokens = getAuthTokens();
  if (!tokens?.accessToken) {
    return undefined;
  }

  const base64Url = tokens?.accessToken?.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    window
      .atob(base64)
      .split('')
      .map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join(''),
  );

  return JSON.parse(jsonPayload);
}

const useAuth = () => {
  const parsed = parseToken();
  const roles = Array.isArray(parsed?.role) ? parsed?.role : [parsed?.role];
  const user: User = {
    id: parsed?.nameid,
    roles: roles,
    email: parsed?.email,
    name: parsed?.name,
    dateOfJoining: parsed?.dateOfJoining ? String(parsed?.dateOfJoining)?.asDateOnly() : undefined,
    employeeId: parsed?.employeeId,
    companyId: parsed?.companyId,
    isCompanyAdmin: roles.includes('CompanyAdmin'),
    isSuperAdmin: roles.includes('SuperAdmin'),
    isEmployee: !!parsed?.employeeId,
  };

  return { user };
};

export default useAuth;
