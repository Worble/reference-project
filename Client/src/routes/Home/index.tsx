import { Fragment, h } from "preact";
import { useEffect, useState } from "preact/hooks";
import { ForumLink } from "../../components/ForumLink";
import { topicLocation } from "../";
import { getSubForumsRequest } from "../../api/requests/getSubForumsRequest";
import type { ISubForumResponse, ISubForumTopic } from "../../api/requests/getSubForumsRequest/types";

export const Home = () => {
	const [subForums, setSubForums] = useState<ISubForumResponse[] | null>(null);

	useEffect(() => {
		const abortController = new AbortController();

		const requestInit: RequestInit = { signal: abortController.signal };
		getSubForumsRequest(requestInit).then((result) => {
			if (result) {
				setSubForums(result);
			}
		});
		return () => abortController.abort();
	}, []);

	if (subForums === null) {
		return <Fragment>Loading...</Fragment>;
	}

	const displayTopic = (topic: ISubForumTopic) => {
		return (
			<ForumLink location={topicLocation(topic.id, topic.title)}>
				<p>{topic.title}</p>
			</ForumLink>
		);
	};

	const displaySubForum = (subForum: ISubForumResponse) => {
		return (
			<Fragment>
				<p>{subForum.title}</p>
				{subForum.topics.length > 0 && (
					<div style={{ marginLeft: "10px" }}>{subForum.topics.map(displayTopic)}</div>
				)}
			</Fragment>
		);
	};

	return (
		<Fragment>
			<h1>Home</h1>
			<h2>SubForums</h2>
			{subForums.length > 0 ? subForums.map(displaySubForum) : <p>No topics found</p>}
		</Fragment>
	);
};
