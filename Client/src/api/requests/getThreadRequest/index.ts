import { baseRequestInit, baseUrl, httpRequest } from "../../base";
import type { IRequestOptions } from "../../base/types";
import type { IThreadResponse } from "./types";

export const getThreadRequest = async (threadId: number, requestInitOverride?: RequestInit) => {
	var options: IRequestOptions = {
		requestInit: baseRequestInit,
		url: `${baseUrl}/threads/${threadId}`,
	};
	const response = await httpRequest(options, requestInitOverride);
	if (response && response.ok) {
		const json = await response.json();
		return json as IThreadResponse;
	}
};
