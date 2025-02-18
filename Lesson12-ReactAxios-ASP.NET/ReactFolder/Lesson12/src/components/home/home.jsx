import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router";

function Home() {
    const [responseData, setResponsedata] = useState();
    const navigate = useNavigate()

    useEffect(() => {
        const fetchUserInfo = async () => {
            try {
                const token = localStorage.getItem("ASPToken");

                if (!token) {
                    navigate('/login')
                    return;
                }

                const response = await axios.get("https://localhost:7072/api/secure/protected", {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                console.log(response)
                setResponsedata(response.data);
            } catch (error) {
                if (error.status === 401)
                    navigate('/login');
                console.log(error)
            }
        };

        fetchUserInfo();
    }, []);

    return (
        <div>
            <p>{responseData}</p>
        </div>
    );
}

export default Home;
