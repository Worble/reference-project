import type { IThreadUser } from "./";

export interface IThreadPost {
	id: number;
	content: string;
	createdDate: Date;
	createdBy: IThreadUser;
}
