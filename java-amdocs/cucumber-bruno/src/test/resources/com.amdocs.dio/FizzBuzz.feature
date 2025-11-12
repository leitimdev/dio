Feature: FizzBuzz
  Print a sequence of N numbers,
  but for multiples of 3 print Fizz
  and for multiples of 5 print Buzz
  and for multiples of 3 and 5 print FizzBuzz


  Background: run in all scenarios
    Given the background flag is set
    And other flag is set

  Scenario Outline: FizzBuzz a number
    Given the user selected the number <Number>
    When the user clicked the FizzBuzz button
    Then the program prints <Result>
    And validate background flag
    Examples:
    | Number | Result |
    | 2      | 2      |
    | 3      | Fizz   |
    | 6      | Fizz   |
    | 5      | Buzz   |
    | 10     | Buzz   |
    | 15     | Fizz   |

    Scenario: FizzBuzz sequence
      Given the user wants a sequence of 15 numbers
      When the user clicked the FizzBuzzSequence button
      Then the program printed:
      | 1     |
      | 2     |
      | Fizz  |
      | 4     |
      | Buzz  |
      | Fizz  |
      | 7     |
      | 8     |
      | Fizz  |
      | Buzz  |
      | 11    |
      | FizzBuzz  |
      And validate background flag

    Scenario: Table Advanced
      Then the table must be converted:
        | Thiago | Address | teste@teste.com | 39 |
        | Thiago2 | Address2 | teste2@teste.com | 40 |
        | Thiago3 | Address3 | teste3@teste.com | 41 |
      And validate background flag

