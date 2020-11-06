import type { LocationType } from "./LocationType";

export type ForumLocation =
	| { type: LocationType.Home }
	| { type: LocationType.AllTopics }
	| { type: LocationType.Login }
	| { type: LocationType.Topic; id: number; slug: string }
	| { type: LocationType.Thread; id: number; slug: string };
