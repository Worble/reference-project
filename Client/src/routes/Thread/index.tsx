import { Fragment, h } from "preact";
import { useContext, useEffect, useState } from "preact/hooks";
import { getThreadRequest } from "../../api/requests/getThreadRequest";
import type { IThreadResponse, IThreadPost } from "../../api/requests/getThreadRequest/types";
import { CurrentUserContext } from "../../providers";
import { AddPost } from "./AddPost";

export const Thread = (props: { id: number; slug: string }) => {
	const { currentUser } = useContext(CurrentUserContext);
	const [thread, setThread] = useState<IThreadResponse | null>(null);
	const [creatingPost, setCreatingPost] = useState<boolean>(false);

	useEffect(() => {
		const abortController = new AbortController();
		const requestInit: RequestInit = { signal: abortController.signal };
		getThreadRequest(props.id, requestInit).then((result) => {
			if (result) {
				setThread(result);
			}
		});
		return () => abortController.abort();
	}, []);

	if (thread === null) {
		return <Fragment>Loading...</Fragment>;
	}

	const displayPost = (post: IThreadPost) => {
		return (
			<div>
				<p>
					{post.content}
					<br />
					by {post.createdBy.username}
				</p>
			</div>
		);
	};

	const onPostAdd = () => {
		setCreatingPost(false);
		getThreadRequest(props.id).then((result) => {
			if (result) {
				setThread(result);
			}
		});
	};

	return (
		<Fragment>
			<h1>Thread: {thread.title}</h1>
			<h2>Posts</h2>
			<div>{thread.posts.map(displayPost)}</div>
			{!creatingPost && (
				<div>
					{currentUser ? (
						<button onClick={() => setCreatingPost(true)}>Add Post</button>
					) : (
						<button disabled={true}>Sign in to post</button>
					)}
				</div>
			)}
			{creatingPost && <AddPost threadId={thread.id} onAdd={onPostAdd} />}
		</Fragment>
	);
};
