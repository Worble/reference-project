import { h, Fragment } from "preact";
import Router, { Link, Route } from "preact-router";
import { Home, Topic, NotFound, homeLocation, loginLocation, Login } from "./routes";
import { ForumLink } from "./components/ForumLink";
import { Thread } from "./routes/Thread";
import { CurrentUserContext, CurrentUserProvider } from "./providers";
import { useContext } from "preact/hooks";

const AppWrapper = () => {
	return (
		<CurrentUserProvider>
			<App />
		</CurrentUserProvider>
	);
};

const App = () => {
	const { currentUser } = useContext(CurrentUserContext);
	return (
		<Fragment>
			{currentUser && <div>Hello {currentUser.username}</div>}
			<nav>
				<ForumLink location={homeLocation}>Home</ForumLink>&nbsp;&nbsp;
				{!currentUser && (
					<Fragment>
						<ForumLink location={loginLocation}>Login</ForumLink>&nbsp;&nbsp;
					</Fragment>
				)}
				<Link href="/this/does/not/exist">404</Link> &nbsp;&nbsp;
			</nav>
			<main>
				<Router>
					<Route path="/" component={Home} />
					<Route path="/login" component={Login} />
					<Route path="/topics/:id/:slug*" component={Topic} />
					<Route path="/threads/:id/:slug*" component={Thread} />
					<NotFound default />
				</Router>
			</main>
			<footer></footer>
		</Fragment>
	);
};

export default AppWrapper;
