import { AiOutlineCar } from "react-icons/ai";
import Search from "./Search";
import Logo from "./Logo";
import Login from "./Login";
import { getCurrentUser } from "../actions/AuthActions";
import UserAction from "./UserAction";

export default async function NavBar() {
    const user = await getCurrentUser();
    return (
        <header className="sticky top-0 z-100 flex justify-between
         bg-white p-5 items-center h-20 text-gray-800 shadow-md">
            <Logo />
            <Search />
            {user ? <UserAction user={user} /> : <Login />}
        </header>
    )
}
