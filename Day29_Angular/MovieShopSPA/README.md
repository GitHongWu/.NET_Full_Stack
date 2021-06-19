# MovieShopSPA

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 12.0.0.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.


# create component
ng g c {componentName}

# create services
ng g s core/services/movie

The generate command and the different sub-commands also have shortcut notations, so the following commands are similar:
   ? ng g c my-new-component: add a component to your application
   ? ng g c books/bookform --flat : flat add component in existing folder without creating specific folder.
   ? ng g cl my-new-class: add a class to your application
   ? ng g d my-new-directive: add a directive to your application
   ? ng g e my-new-enum: add an enum to your application
   ? ng g m my-new-module: add a module to your application
   ? ng g p my-new-pipe: add a pipe to your application
   ? ng g s my-new-service: add a service to your application
   ? ng g c --skipTests=true component-name
   ? ng g m subfolder/modulename --routing --flat  New Module with routing 
   

ng g module app-routing --module app --flat   Create an AppRouting module in the /app folder to contain the routing configuration

ng g module heroes/heroes --module app --flat --routing Create a HeroesModule with routing in the heroes folder and register it with the root AppModule. This is where you'll be implementing the hero management.