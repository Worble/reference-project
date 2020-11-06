import { baseRequestInit, baseUrl, httpRequest } from "../../base";
import type { IRequestOptions } from "../../base/types";
import type { ITopicResponse } from "./types";

export const getTopicRequest = async (topicId: number, requestInitOverride?: RequestInit) => {
	var options: IRequestOptions = {
		requestInit: baseRequestInit,
		url: `${baseUrl}/topics/${topicId}`,
	};
	const response = await httpRequest(options, requestInitOverride);
	if (response && response.ok) {
		const json = await response.json();
		return json as ITopicResponse;
	}
};
