<?php

namespace App\Games;

use SilverStripe\ORM\DataObject;
use SilverStripe\Forms\DropdownField;
use SilverStripe\Security\Permission;
use App\CharacterDatabase\CharacterHat;
use App\CharacterDatabase\CharacterTop;
use App\CharacterDatabase\CharacterEyes;
use App\CharacterDatabase\CharacterHair;
use App\CharacterDatabase\CharacterPart;
use App\CharacterDatabase\CharacterMouth;
use App\CharacterDatabase\CharacterBottom;
use App\CharacterDatabase\CharacterSkinColor;
use App\Users\UserData;
use DateTime;

/**
 * Class \App\Database\Experience
 *
 * @property int $Points
 * @property int $UserID
 * @property int $ParentID
 * @method \App\Users\UserData User()
 * @method \App\Games\Game Parent()
 */
class HighScore extends DataObject
{
    private static $db = [
        "Points" => "Int"
    ];

    private static $has_one = [
        "User" => UserData::class,
        "Parent" => Game::class,
    ];

    private static $summary_fields = [
        "User.Nickname" => "User",
        "Parent.Title" => "Game",
        "Points" => "Points",
    ];

    private static $field_labels = [
        "Nickname" => "Nickname",
    ];

    private static $default_sort = "Created DESC";

    private static $table_name = "HighScore";

    private static $singular_name = "Highscore";
    private static $plural_name = "Highscores";

    private static $url_segment = "highscore";
}
