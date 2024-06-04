import { useAdminEndpointQuery, usePublicEndpointQuery } from "./services/mainApi";

const Home = ():JSX.Element => {
  const adminQuery = useAdminEndpointQuery();
  const publicQuery = usePublicEndpointQuery();
  return (

    <div>
      <div>{publicQuery.data}</div>
      <div>{adminQuery.data}</div>
    </div>
  );
}
export default Home;
