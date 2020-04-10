public enum GameState
{
	MainMenu,
	Pass,
	Cave,
	Village,
	GameOver
}

public enum AIState
{
	Idle,
	Move,
	Attack,
	Flee
}

public enum ItemType
{
    Resource,
    Weapon,
    Tool,
    Consumable
}

public enum InventoryID
{
    player,
    sled
}

public enum ItemID
{
	wood,
	stick,
	rock,
	meatRabbit,
	meatFox,
	meatDog,
	meatWolf,
	meatBear,
	meatYeti,
	meatHuman,
	meatFish,
	furRabbit,
	furFox,
	furDog,
	furWolf,
	furBear,
	furYeti,
	medPot,
	medFlowerTulip,
	medFlowerRose,
	medFlowerOrchid,
	medHerbSweet,
	medHerbCinna,
	medHerbCocoa,
	medScroll,
	medScrollMyth,
	potionVial,
	rope,
	Sword,
	fishingRod,
	torch,
	axe,
	bow,
	arrows,
	climbPick,
	ore,
	coatRabbit,
	coatFox,
	coatDog,
	coatWolf,
	coatBear,
	coatPolar,
	coatGrizzly,
	coatYeti,
	coatMythYeti,
	//leave this at bottom of list
	count
}