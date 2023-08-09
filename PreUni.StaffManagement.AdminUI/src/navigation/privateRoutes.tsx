/** 
  All of the routes for the Otis Admin PRO React are added here,
  You can add a new route, customize the routes and delete the routes here.

  Once you add a new route on this file it will be visible automatically on
  the Sidenav.

  For adding a new route you can follow the existing routes in the routes array.
  1. The `type` key with the `collapse` value is used for a route.
  2. The `type` key with the `title` value is used for a title inside the Sidenav. 
  3. The `type` key with the `divider` value is used for a divider between Sidenav items.
  4. The `name` key is used for the name of the route on the Sidenav.
  5. The `key` key is used for the key of the route (It will help you with the key prop inside a loop).
  6. The `icon` key is used for the icon of the route on the Sidenav, you have to add a node.
  7. The `collapse` key is used for making a collapsible item on the Sidenav that contains other routes
  inside (nested routes), you need to pass the nested routes inside an array as a value for the `collapse` key.
  8. The `route` key is used to store the route location which is used for the react router.
  9. The `href` key is used to store the external links location.
  10. The `title` key is only for the item with the type of `title` and its used for the title text on the Sidenav.
  10. The `component` key is used to store the component of its route.
*/

import Icon from "@mui/material/Icon";
import PAGES from "./pages.ts";
import UserDetail from "../features/user-detail/userDetail";
import Users from "../features/users/users";
import JobApplication from "features/job-application/jobApplication.tsx";
import JobApplicationDetail from "features/job-application-detail/jobApplicationDetail.tsx";

const routes = [
  {
    type: "tab",
    name: "Users",
    key: "users",
    route: PAGES.users,
    icon: <Icon fontSize="medium">people</Icon>,
    component: <Users />,
  },
  // single pages
  {
    type: "page",
    key: "new-user",
    route: PAGES.newUser,
    component: <UserDetail />,
  },
  {
    type: "page",
    key: "edit-user",
    route: `${PAGES.editUser}/:id`,
    component: <UserDetail />,
  },
  {
    type: "tab",
    name: "Job Applications",
    key: "job-application",
    route: PAGES.jobApplication,
    icon: <Icon fontSize="medium">work</Icon>,
    component: <JobApplication />,
  },
  // single pages
  {
    type: "page",
    key: "new-job-application",
    route: PAGES.newJobApplication,
    component: <JobApplicationDetail />,
  },
  {
    type: "page",
    key: "edit-job-application",
    route: `${PAGES.editJobApplication}/:id`,
    component: <JobApplicationDetail />,
  },
  {
    type: "logout",
    key: "logout",
    name: "Log out",
    icon: <Icon fontSize="medium">logout</Icon>,
  },
];

export default routes;
