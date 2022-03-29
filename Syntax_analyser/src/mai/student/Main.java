package mai.student;

import java.io.FileReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
//        try (Scanner scanner = new Scanner(System.in)) {
//            SyntaxAnanlyser anal = new SyntaxAnanlyser(extractLexemes(scanner.nextLine()));
//            anal.analyse();
//        } catch (Exception e) {
//            System.out.println(e.getMessage());
//        }
        try (Scanner scanner = new Scanner(new FileReader("test.txt"))) {
            while (scanner.hasNext()) {
                String input = scanner.nextLine();
                try {
                    SyntaxAnanlyser anal = new SyntaxAnanlyser(extractLexemes(input));
                    anal.analyse();
                    System.out.println(input + "\n" + anal.getTree() + "\n" + anal.getResult() + "\n!!!!!!!!!!!!!!!!!\n");
                } catch (Exception ex) {
                    System.out.println(input + "\n" + ex.getMessage() + "\n!!!!!!!!!!!!!!!!!\n");
                }
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    public static ArrayList<Token> extractLexemes(String str) throws Exception {
        HashMap<Integer, String> finalStatesMapping = new HashMap<>();
        ArrayList<Token> result = new ArrayList<>();

        try (Scanner scanner = new Scanner(new FileReader("tokens.txt"))) {

            while (scanner.hasNext()) {
                finalStatesMapping.put(scanner.nextInt(), scanner.nextLine());
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        int state = 0, prevState = 0, startIndex = 0, prevFinalState = -1, prevFinalStateIndex = -1;

        for (int i = 0; i < str.length(); ++i) {
            prevState = state;
            switch (state) {
                case 0:
                    if (str.charAt(i) >= 'a' && str.charAt(i) <= 'z' || str.charAt(i) == '_') {
                        state = 7;
                    } else if (str.charAt(i) == '(') {
                        state = 8;
                    } else if (str.charAt(i) == ')') {
                        state = 9;
                    } else if (str.charAt(i) == ',') {
                        state = 10;
                    } else if (str.charAt(i) == '+' || str.charAt(i) == '-' || str.charAt(i) == '*' ||
                            str.charAt(i) == '/' || str.charAt(i) == '^') {
                        state = 6;
                    } else if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 1;
                    } else {
                        state = -1;
                    }
                    break;
                case 1:
                    if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 1;
                    } else if (str.charAt(i) == '.') {
                        state = 2;
                    } else if (str.charAt(i) == 'e' || str.charAt(i) == 'E') {
                        state = 3;
                    } else {
                        state = -1;
                    }
                    break;
                case 2:
                    if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 2;
                    } else if (str.charAt(i) == 'e' || str.charAt(i) == 'E') {
                        state = 3;
                    } else {
                        state = -1;
                    }
                    break;
                case 3:
                    if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 5;
                    } else if (str.charAt(i) == '+' || str.charAt(i) == '-') {
                        state = 4;
                    } else {
                        state = -1;
                    }
                    break;
                case 4:
                    if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 5;
                    } else {
                        state = -1;
                    }
                    break;
                case 5:
                    if (str.charAt(i) >= '0' && str.charAt(i) <= '9') {
                        state = 5;
                    } else {
                        state = -1;
                    }
                    break;
                case 7:
                    if (str.charAt(i) >= 'a' && str.charAt(i) <= 'z' || str.charAt(i) == '_' || str.charAt(i) >= '0'
                            && str.charAt(i) <= '9') {
                        state = 7;
                    } else {
                        state = -1;
                    }
                    break;
                default:
                    state = -1;
                    break;
            }

            if (i == str.length() - 1) {
                prevState = state;
                state = -1;
            }

            if (finalStatesMapping.containsKey(state)) {
                prevFinalState = state;
                prevFinalStateIndex = i;
            }

            if (state == -1) {
                if (finalStatesMapping.containsKey(prevState) && (str.charAt(i) == ' ' || i == str.length() - 1)) {
                    if (i == str.length() - 1) {
//                        System.out.println(finalStatesMapping.get(prevState) + " " + str.substring(startIndex));
                        result.add(new Token(prevState, str.substring(startIndex)));
                    } else {
//                        System.out.println(finalStatesMapping.get(prevState) + " " + str.substring(startIndex, i));
                        result.add(new Token(prevState, str.substring(startIndex, i)));
                    }
                } else if (prevFinalState != -1) {
//                    System.out.println(finalStatesMapping.get(prevFinalState) + " " + str.substring(startIndex, prevFinalStateIndex + 1));
                    result.add(new Token(prevFinalState, str.substring(startIndex, prevFinalStateIndex + 1)));
                    i = prevFinalStateIndex;
                } else {
                    throw new Exception("Unexpected symbol " + str.charAt(i));
                }

                prevFinalState = -1;
                state = 0;
                startIndex = i + 1;
            }
        }

        return result;
    }
}
