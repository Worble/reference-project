import type { ITopicThread } from "./";

export interface ITopicResponse {
	id: number;
	title: string;
	children: ITopicResponse[];
	threads: ITopicThread[];
	parent?: ITopicResponse;
}
