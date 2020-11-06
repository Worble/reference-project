import type { IRequestOptions } from "./types";

export const httpRequest = async (requestOptions: IRequestOptions, requestInitOverride?: RequestInit) => {
	const requestInit: RequestInit = { ...requestOptions.requestInit, ...requestInitOverride };
	try {
		return await fetch(requestOptions.url, requestInit);
	} catch (err) {
		// Avoid showing an error message if the fetch was aborted
		if (err.name !== "AbortError") {
			throw err;
		}
	}
};
