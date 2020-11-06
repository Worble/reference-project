import { createContext } from "preact";
import type { IAuthenticationResponse } from "../../api/requests/authenticateRequest/types";

export const CurrentUserContext = createContext<{
	currentUser: IAuthenticationResponse | null;
	setCurrentUser: (value: IAuthenticationResponse) => void;
}>({ currentUser: null, setCurrentUser: () => {} });
