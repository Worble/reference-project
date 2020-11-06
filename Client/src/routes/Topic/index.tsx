import { Fragment, h } from "preact";
import { useEffect, useState } from "preact/hooks";
import { ForumLink } from "../../components/ForumLink";
import { threadLocation } from "../";
import { getTopicRequest } from "../../api/requests/getTopicRequest";
import type { ITopicResponse, ITopicThread } from "../../api/requests/getTopicRequest/types";

export const Topic = (props: { id: number; slug: string }) => {
	const [topic, setTopic] = useState<ITopicResponse | null>(null);

	useEffect(() => {
		const abortController = new AbortController();
		const requestInit: RequestInit = { signal: abortController.signal };
		getTopicRequest(props.id, requestInit).then((result) => {
			if (result) {
				setTopic(result);
			}
		});
		return () => abortController.abort();
	}, []);

	if (topic === null) {
		return <Fragment>Loading...</Fragment>;
	}

	const displayThread = (thread: ITopicThread) => {
		return <ForumLink location={threadLocation(thread.id, thread.title)}>{thread.title}</ForumLink>;
	};

	return (
		<Fragment>
			<h1>Topic: {topic.title}</h1>
			<h2>Threads</h2>
			{topic.threads.length > 0 ? topic.threads.map(displayThread) : <p>There are no threads</p>}
		</Fragment>
	);
};
