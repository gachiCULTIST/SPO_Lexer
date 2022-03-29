package mai.student;

import java.io.FileReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

public class SyntaxAnanlyser {

    private static final int EXPRESSION = 10;
    private static final int PLUS = 11;
    private static final int MINUS = 12;
    private static final int MULTIPLY = 13;
    private static final int DIVIDE = 14;
    private static final int POW = 15;
    private static final int NEG = 16;

    private ArrayList<Token> tokens;
    private int pointer = 0;
    private TreeNode tree;
    private double result;

    private HashMap<String, Double> pWords;

    public SyntaxAnanlyser (ArrayList<Token> tokens) {

        this.tokens = tokens;
        pWords = new HashMap<>();

        try (Scanner scanner = new Scanner(new FileReader("pWords.txt"))) {
            while (scanner.hasNext()) {
                pWords.put(scanner.next(), scanner.nextDouble());
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    public void analyse () throws Exception {
            tree = E();
            if (pointer < tokens.size()) {
                throw new Exception("Unexpected token " + tokens.get(pointer).lexeme);
            }
//            System.out.println(treeToStr(tree));
            result = solveExpression(tree);
//            System.out.println(result);
    }

    public String getTree() {
        if (tree != null) {
            return treeToStr(tree).toString();
        } else {
            return "Выражение не проанализировано!";
        }
    }

    public double getResult () {
        if (tree != null) {
            return result;
        } else {
            return 0;
        }
    }

    private double solveExpression (TreeNode node) throws Exception {
        if (node.token.token == 8) {
            return solveExpression(node.children.get(0));
        } else if (node.token.token == 7) {
            if (pWords.containsKey(node.token.lexeme)) {
                return pWords.get(node.token.lexeme);
            }
            throw new Exception("Unknown id " + node.token.lexeme);
        } if (node.token.token == 1 || node.token.token == 2 || node.token.token == 5) {
            return Double.parseDouble(node.token.lexeme);
        } else {
            double left = solveExpression(node.children.get(0)), right = 0;
            if (node.token.token != NEG) {
                right = solveExpression(node.children.get(1));
            }

            switch (node.token.token) {
                case PLUS:
                    return left + right;
                case MINUS:
                    return left - right;
                case MULTIPLY:
                    return left * right;
                case DIVIDE:
                    return left / right;
                case POW:
                    return Math.pow(left, right);
                case NEG:
                    return -left;
                default:
                    return 0;
            }
        }
    }

    private StringBuilder treeToStr (TreeNode node) {
        StringBuilder result = new StringBuilder();

        if (node.children.size() != 0) {

            if (node.token.token != 8) {
                result.append(node.token.lexeme + "(");

                for (int i = 0; i < node.children.size(); ++i) {
                    result.append(treeToStr(node.children.get(i)));
                    if (i != node.children.size() - 1) {
                        result.append(", ");
                    }
                }

                result.append(")");
            } else { // Особая ветка для скобочек
                result.append(treeToStr(node.children.get(0)));
            }

        } else {
            result.append(node.token.lexeme);
        }

        return result;
    }

    private TreeNode E() throws Exception {
        TreeNode node = new TreeNode(new Token(EXPRESSION, "expr"));

        TreeNode n = T();
        if (n != null) {
            node.children.add(n);
        }

        n = E2(n);
        if (n != null) {
            node.children.add(n);
        }

        return n;
    }

    private TreeNode E2 (TreeNode left) throws Exception {
        TreeNode result = left;

        if (pointer < tokens.size()) {
            Token sym = tokens.get(pointer);

            if (sym.lexeme.equals("+")) {
                result = new TreeNode(new Token(PLUS, "+"));
                result.children.add(left);
                ++pointer;

                TreeNode n = T();
                if (n != null) {
                    result.children.add(n);
                }

                result = E2(result);
            } else if (sym.lexeme.equals("-")) {
                result = new TreeNode(new Token(MINUS, "-"));
                result.children.add(left);
                ++pointer;

                TreeNode n = T();
                if (n != null) {
                    result.children.add(n);
                }

                result = E2(result);
            }
        }

        return result;
    }

    private TreeNode T () throws Exception {
        TreeNode node = F();
        return T2(node);
    }

    private TreeNode T2 (TreeNode left) throws Exception {
        TreeNode result = left;

        if (pointer < tokens.size()) {
            Token sym = tokens.get(pointer);

            if (sym.lexeme.equals("*")) {
                result = new TreeNode(new Token(MULTIPLY, "*"));
                result.children.add(left);
                ++pointer;

                TreeNode n = F();
                if (n != null) {
                    result.children.add(n);
                }

                result = T2(result);
            } else if (sym.lexeme.equals("/")) {
                result = new TreeNode(new Token(DIVIDE, "/"));
                result.children.add(left);
                ++pointer;

                TreeNode n = F();
                if (n != null) {
                    result.children.add(n);
                }

                result = T2(result);
            }
        }

        return result;
    }

    private TreeNode F () throws Exception {
        TreeNode n1 = V();
        return F2(n1);
    }

    private TreeNode F2 (TreeNode left) throws Exception {

        if (pointer < tokens.size()) {
            Token sym = tokens.get(pointer);

            if (sym.lexeme.equals("^")) {
                TreeNode result = new TreeNode(new Token(POW, "^"));
                result.children.add(left);
                ++pointer;
                TreeNode n = F();
                if (n != null) {
                    result.children.add(n);
                }
                return result;
            }
        }

        return left;
    }

    private TreeNode V () throws Exception {
        TreeNode result = null;
        if (pointer < tokens.size()) {
            Token sym = tokens.get(pointer);

            if (sym.lexeme.equals("(")) {
                result = new TreeNode(sym);
                ++pointer;

                TreeNode node = E();
                if (node != null) {
                    result.children.add(node);
                }

                if (pointer < tokens.size()) {
                    sym = tokens.get(pointer);
                } else {
                    throw new Exception("Not found token )");
                }
                if (sym.lexeme.equals(")")) {
                    result.children.add(new TreeNode(sym));
                    ++pointer;
                } else {
                    throw new Exception("Not found token )");
                }
            } else if (sym.token == 1 || sym.token == 2 || sym.token == 5) {
                result = new TreeNode(sym);
                ++pointer;
            } else if (sym.token == 7) {
                result = new TreeNode(sym);
                ++pointer;
            } else if (sym.lexeme.equals("-")) {
                result = new TreeNode(new Token(NEG, "-"));
                ++pointer;
                TreeNode node = V();
                if (node != null) {
                    result.children.add(node);
                }
            } else {
                throw new Exception("Unexpected token " + sym.lexeme);
            }
        } else {
            throw new Exception("Unexpected eof");
        }

        return result;
    }
}