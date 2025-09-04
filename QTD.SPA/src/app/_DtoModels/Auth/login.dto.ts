/* "Definite Assignment Assertion" (!) to tell TypeScript that we know this value
will be defined in some way by the time we use it; and, that TypeScript should
not worry about the value until then. 

This is Just like a POCO class in c# to bind values
*/

export class LoginModel {
  username: string | '';
  password: string | '';
}
