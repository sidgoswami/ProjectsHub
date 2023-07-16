# Steps to configure Jest for a new Project:

### Step 1: 
    npm install -D jest @types/jest jest-preset-angular
### Step 2:
    npm uninstall karma karma-chrome-launcher karma-coverage-istanbul-reporter karma-jasmine karma-jasmine-html-reporter jasmine-core @types/jasmine @types/jasminewd2
### Step 3:
    npm install --save-dev @angular-builders/jest
### Step 4: 
    Change the test command in angular.json to the below text:
        "test": {
             "builder": "@angular-builders/jest:run"
        }
### Step 5:
    Create the jest.config.ts on the project root folder
        module.exports = {
            preset: 'jest-preset-angular',
            roots: ['src'],
            testMatch: ['**/+(*.)+(spec).+(ts)'],
            setupFilesAfterEnv: ['<rootDir>/src/setup-jest.ts'],
        };
### Step 6:
    Create the setup-jest.ts file inside the src folder with the below content      
        import 'jest-preset-angular/setup-jest';
