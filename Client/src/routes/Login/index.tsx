import { h, Fragment } from "preact";
import { useContext, useState } from "preact/hooks";
import { CurrentUserContext } from "../../providers";
import { route } from "preact-router";
import { authenticateRequest } from "../../api/requests/authenticateRequest";

export const Login = () => {
	const { currentUser, setCurrentUser } = useContext(CurrentUserContext);
	const [emailAddressOrUsername, setUsername] = useState("");
	const [password, setPassword] = useState("");

	const onSubmit = async (event: h.JSX.TargetedEvent<HTMLFormElement, Event>) => {
		event.preventDefault();
		var result = await authenticateRequest({ emailAddressOrUsername, password });
		if (result) {
			setCurrentUser(result);
		}
	};

	if (currentUser) {
		route("/");
	}

	return (
		<Fragment>
			<h1>Login</h1>
			<form onSubmit={onSubmit}>
				<div>
					<label>
						Username or Email Address
						<br />
						<input
							value={emailAddressOrUsername}
							onInput={(event) => setUsername(event.currentTarget.value)}
						/>
					</label>
				</div>
				<div>
					<label>
						Password
						<br />
						<input
							type="password"
							value={password}
							onInput={(event) => setPassword(event.currentTarget.value)}
						/>
					</label>
				</div>
				<button>Submit</button>
			</form>
		</Fragment>
	);
};
