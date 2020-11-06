import { h } from "preact";
import type { ComponentChildren } from "preact";
import { useState } from "preact/hooks";
import { CurrentUserContext } from "./CurrentUserContext";
import type { IAuthenticationResponse } from "../../api/requests/authenticateRequest/types";

export const CurrentUserProvider = (props: { children: ComponentChildren }) => {
	const [value, setValue] = useState<IAuthenticationResponse | null>(null);

	return (
		<CurrentUserContext.Provider value={{ currentUser: value, setCurrentUser: setValue }}>
			{props.children}
		</CurrentUserContext.Provider>
	);
};
export * from "./CurrentUserContext";
