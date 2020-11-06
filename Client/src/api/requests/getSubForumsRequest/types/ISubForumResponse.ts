import type { ISubForumTopic } from "./";

export interface ISubForumResponse {
	id: number;
	title: string;
	topics: ISubForumTopic[];
}
