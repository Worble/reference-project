import { Fragment, h } from "preact";
import { useState } from "preact/hooks";
import { addPostRequest } from "../../api/requests/addPostRequest";

export const AddPost = (props: { threadId: number; onAdd?: () => void }) => {
	const [postText, setPostText] = useState<string>("");
	const [performingRequest, setPerformingRequest] = useState<boolean>(false);

	const AddPost = (event: h.JSX.TargetedEvent<HTMLFormElement, Event>) => {
		event.preventDefault();
		setPerformingRequest(true);
		addPostRequest({ content: postText, threadId: props.threadId }).then(() => {
			setPerformingRequest(false);
			if (props.onAdd) {
				props.onAdd();
			}
		});
	};

	return (
		<form onSubmit={AddPost}>
			<textarea value={postText} onInput={(event) => setPostText(event.currentTarget.value)}></textarea>
			<br />
			<button disabled={performingRequest}>{performingRequest ? "Submitting..." : "Add Post"}</button>
		</form>
	);
};
