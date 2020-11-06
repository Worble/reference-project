import { h } from "preact";
import { Link } from "preact-router";
import slugify from "slugify";
import type { ForumLocation } from "./Location";
import { LocationType } from "./LocationType";

export const ForumLink = (props: { activeClassName?: string; location: ForumLocation } & preact.JSX.HTMLAttributes) => {
	var slugOptions = {
		lower: true,
	};
	let href: () => string = () => {
		switch (props.location.type) {
			case LocationType.Home:
				return "/";
			case LocationType.AllTopics:
				return "/topics";
			case LocationType.Topic:
				return `/topics/${props.location.id}/${slugify(props.location.slug, slugOptions)}`;
			case LocationType.Thread:
				return `/threads/${props.location.id}/${slugify(props.location.slug, slugOptions)}`;
			case LocationType.Login:
				return "/login";
		}
	};
	return <Link {...props} href={href()}></Link>;
};
