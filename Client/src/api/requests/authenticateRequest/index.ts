import { baseRequestInit, baseUrl, httpRequest } from "../../base";
import type { IRequestOptions } from "../../base/types";
import type { IAuthenticateRequestBody, IAuthenticationResponse } from "./types";

export const authenticateRequest = async (body: IAuthenticateRequestBody, requestInitOverride?: RequestInit) => {
	var options: IRequestOptions = {
		url: `${baseUrl}/authentication`,
		requestInit: {
			...baseRequestInit,
			method: "POST",
			body: JSON.stringify(body),
		},
	};
	const response = await httpRequest(options, requestInitOverride);
	if (response && response.ok) {
		const json = await response.json();
		return json as IAuthenticationResponse;
	}
};
