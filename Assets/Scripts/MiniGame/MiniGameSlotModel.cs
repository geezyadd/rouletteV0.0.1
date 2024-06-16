
public class MiniGameSlotModel 
{
    private MiniGameSlotType _type;
    private SkillType _skillType;

    public MiniGameSlotType MiniGameType
    {
        get =>
            _type; 
        set => 
            _type = value;
    }

    public SkillType SkillType
    {
        get =>
            _skillType;
        set =>
            _skillType = value;
    }
}
