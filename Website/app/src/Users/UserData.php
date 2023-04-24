<?php

namespace App\Users;

use SilverStripe\ORM\DataObject;
use SilverStripe\Security\Permission;
use App\CharacterDatabase\CharacterHat;
use App\CharacterDatabase\CharacterTop;
use App\CharacterDatabase\CharacterEyes;
use App\CharacterDatabase\CharacterHair;
use App\CharacterDatabase\CharacterPart;
use App\CharacterDatabase\CharacterMouth;
use App\CharacterDatabase\CharacterBottom;
use App\CharacterDatabase\CharacterSkinColor;

/**
 * Class \App\Database\Experience
 *
 * @property string $Nickname
 * @property int $XP
 * @property string $UserKey
 * @property int $SelectedSkinColor
 * @property int $SelectedEyes
 * @property int $SelectedMouth
 * @property int $SelectedHair
 * @property int $SelectedBottom
 * @property int $SelectedTop
 * @property int $SelectedHat
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterPart[] AquiredCharacterParts()
 */
class UserData extends DataObject
{
    private static $db = [
        "Nickname" => "Varchar(255)",
        "XP" => "Int",
        "UserKey" => "Varchar(255)",
        "SelectedSkinColor" => "Int",
        "SelectedEyes" => "Int",
        "SelectedMouth" => "Int",
        "SelectedHair" => "Int",
        "SelectedBottom" => "Int",
        "SelectedTop" => "Int",
        "SelectedHat" => "Int",
    ];

    private static $many_many = [
        "AquiredCharacterParts" => CharacterPart::class,
    ];

    private static $summary_fields = [
        "Title" => "Title",
    ];

    private static $field_labels = [
        "Title" => "Title",
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "UserData";

    private static $singular_name = "UserData";
    private static $plural_name = "UserDatas";

    private static $url_segment = "userdata";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
