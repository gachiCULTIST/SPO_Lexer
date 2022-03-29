package mai.student;

import java.util.ArrayList;

public class TreeNode {
    public Token token;
    public ArrayList<TreeNode> children;

    public TreeNode (Token token) {
        this.token = token;
        children = new ArrayList<>();
    }
}
