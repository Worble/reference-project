import { baseRequestInit, baseUrl, httpRequest } from "../../base";
import type { IRequestOptions } from "../../base/types";
import type { ISubForumResponse } from "./types";

export const getSubForumsRequest = async (requestInitOverride?: RequestInit) => {
	var options: IRequestOptions = {
		requestInit: baseRequestInit,
		url: `${baseUrl}/subforums`,
	};
	const response = await httpRequest(options, requestInitOverride);
	if (response && response.ok) {
		const json = await response.json();
		return json as ISubForumResponse[];
	}
};
