import { useEffect, useState } from "react";

interface JwtPayload {
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?: string[];
  role?: string[];
}

function decodeJwt(token: string): JwtPayload | null {
  try {
    const parts = token.split(".");
    if (parts.length !== 3) return null;
    const payload = parts[1];
    const base64 = payload.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
        .join("")
    );
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

export function useAdminRole() {
  const [isAdmin, setIsAdmin] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      setIsAdmin(false);
      setLoading(false);
      return;
    }
    const decoded = decodeJwt(token);
    if (!decoded) {
      setIsAdmin(false);
      setLoading(false);
      return;
    }
    const rawRoles =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
      decoded.role ||
      [];
    const roles = Array.isArray(rawRoles) ? rawRoles : [rawRoles];
    setIsAdmin(roles.some((r) => r.toLowerCase() === "admin"));
    setLoading(false);
  }, []);

  return { isAdmin, loading };
}
