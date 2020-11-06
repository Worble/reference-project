import type { IThreadPost, IThreadUser } from ".";

export interface IThreadResponse {
	id: number;
	title: string;
	createdBy: IThreadUser;
	posts: IThreadPost[];
	createdDate: Date;
}
