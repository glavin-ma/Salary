import { environment } from "../environments/environment";
import { ACCESS_TOKEN_KEY } from "./services";

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

export const jwtSettings = {
  config: {
    tokenGetter: tokenGetter,
    whitelistedDomains: environment.tokenWhiteListedDomains
  }
};
