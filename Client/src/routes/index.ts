import type { ForumLocation } from "../components/ForumLink/Location";
import { LocationType } from "../components/ForumLink/LocationType";

export * from "./Home";
export * from "./NotFound";
export * from "./Topic";
export * from "./Login";

export const homeLocation: ForumLocation = { type: LocationType.Home };
export const allTopicsLocation: ForumLocation = { type: LocationType.AllTopics };
export const topicLocation: (id: number, slug: string) => ForumLocation = (id, slug) => {
	return { type: LocationType.Topic, id, slug };
};
export const threadLocation: (id: number, slug: string) => ForumLocation = (id, slug) => {
	return { type: LocationType.Thread, id, slug };
};
export const loginLocation: ForumLocation = { type: LocationType.Login };
