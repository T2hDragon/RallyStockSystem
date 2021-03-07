Ott Tänak decides to improve their new teams service park performance. Problems so far has been:

* When a rally car arrives at service park - parts are not ready for required repair tasks.

* When a team has to resupply their stock of spares, it's unclear how many different items are still in stock in teams storage vans.

Design and implement Razor pages website to help Ott and Hunday out.

Implement a simple stock system - every item (nut, bolt, spare gearbox, etc) has category, location, current quantity and optimal quantity, price.

Predefined jobs - Name, list of items/quantity needed.

When Martin Järveoja calls after loop of stages and yells - we should replace alternator, its wonky - you can check if the team is able to perform this task. If yes, then reduce quantities in stock.

When rally is over:

* produce a list of items/quantity needed to restore normal stock levels.

* make report of performed service jobs (list of jobs with materials spent, cost of every job, overall materials spent/cost)

Provide search for items in stock - based on name, quantity, category. Allow several names, and both inclusion and exclusion (“bolt, !nut”).


Generally speaking - these are only broad guidelines. Please write a solution, that you would like to present to the world as your best effort in programming and app-designing (UX is the key).

Implement as many features, as you can think of - but apply MVP approach (minimum viable product).

To be implemented as Razor Pages based Web app. No ViewBags! Nullable Reference Types have to be enabled solution wide.