import { baseUrl, baseRequestInit, httpRequest } from "../../base";
import type { IRequestOptions } from "../../base/types";
import type { IAddPostRequestBody } from "./types";

export const addPostRequest = async (body: IAddPostRequestBody, requestInitOverride?: RequestInit) => {
	var options: IRequestOptions = {
		url: `${baseUrl}/posts/create`,
		requestInit: {
			...baseRequestInit,
			method: "POST",
			body: JSON.stringify(body),
		},
	};
	await httpRequest(options, requestInitOverride);
};
